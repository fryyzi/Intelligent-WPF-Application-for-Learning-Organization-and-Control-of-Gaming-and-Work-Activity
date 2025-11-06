import socket
import speech_recognition as sr

HOST = '127.0.0.1'
PORT = 5000

recognizer = sr.Recognizer()
mic = sr.Microphone()

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen(1)
    print("🎤 Voice server running...")
    conn, addr = s.accept()
    print(f"Connected to: {addr}")

    with conn:
        while True:
            try:
                with mic as source:
                    recognizer.adjust_for_ambient_noise(source)
                    print("Listening...")
                    audio = recognizer.listen(source)
                    text = recognizer.recognize_google(audio, language="uk-UA")
                    print("Heard:", text)

                    conn.sendall(text.encode('utf-8'))
            except Exception as e:
                print("Error:", e)
