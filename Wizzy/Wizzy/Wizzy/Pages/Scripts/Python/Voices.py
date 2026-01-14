import speech_recognition as sr
from gtts import gTTS
from playsound import playsound
import socket
import time
import os

HOST = "127.0.0.1"
PORT = 5000

with open("F:\programing\Project\Wizzy\Wizzy\Commands\ToolsCommands.txt", "r", encoding="utf-8") as f:
    commands = [line.strip() for line in f.readlines()]

def speak(text, lang="uk"):
    print(f"💬 Відповідь: {text}")
    tts = gTTS(text=text, lang=lang, tld="co.uk")
    filename = "voice.mp3"
    tts.save(filename)
    playsound(filename)
    os.remove(filename)

def recognize_and_send():
    recognizer = sr.Recognizer()
    mic = sr.Microphone()

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind((HOST, PORT))
        s.listen(1)
        print(f"🟢 Сервер слухає на {HOST}:{PORT}")
        conn, addr = s.accept()
        print("🔗 Підключено:", addr)

        with conn:
            while True:
                try:
                    with mic as source:
                        recognizer.adjust_for_ambient_noise(source)
                        print("🎤 Скажи команду...")
                        audio = recognizer.listen(source, timeout=5, phrase_time_limit=5)
                        text = recognizer.recognize_google(audio, language="uk-UA").lower()
                        print(f"🗣️ Розпізнано: {text}")
                        conn.sendall(text.encode('utf-8'))

                        if "браузер" in text:
                            speak("Відкриваю браузер")
                        elif "youtube" in text or "ютуб" in text:
                            speak("Відкриваю ютуб")
                        elif "таймер" in text:
                            speak("Запускаю таймер")
                        elif "конвертер" in text:
                            speak("Відкриваю конвертер")
                        else:
                            speak("Команду не розпізнано, спробуйте ще раз")

                except sr.UnknownValueError:
                    speak("Не розчув команду")
                except sr.WaitTimeoutError:
                    pass
                except Exception as e:
                    print(f"❌ Помилка: {e}")
                    time.sleep(1)

if __name__ == "__main__":
    recognize_and_send()
