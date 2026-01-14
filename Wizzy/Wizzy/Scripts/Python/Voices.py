import socket
import speech_recognition as sr

HOST = '127.0.0.1'
PORT = 5000

recognizer = sr.Recognizer()
mic = sr.Microphone()

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen(1)
    print(f"🟢 Сервер з хотінгом: {HOST} та з портом {PORT} запущений")
    conn, addr = s.accept()
    print(f"Приєднуюсь до адресу: {addr}")
    print("Говоріть: ")

    with conn:
        while True:
            with mic as source:
                    recognizer.adjust_for_ambient_noise(source)
                    
                    ##print("Listening...")
                    audio = recognizer.listen(source)
                    text = recognizer.recognize_google(audio, language="uk-UA")
                    ##print("Heard:", text)

                    conn.sendall(text.encode('utf-8'))