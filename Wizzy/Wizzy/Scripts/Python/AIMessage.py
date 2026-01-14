import socket
import json
import google.generativeai as genai

# 🔑 ЗАМІНИ НА СВІЙ API KEY
genai.configure(api_key="AIzaSyD82mvQGIU8BHcghr2i9I4Zn9NmSSzbzqE")

model = genai.GenerativeModel("models/gemini-2.0-flash-exp")

HOST = "127.0.0.1"
PORT = 5050

def handle_client(conn):
    with conn:
        while True:
            data = conn.recv(4096)
            if not data:
                break

            try:
                req = json.loads(data.decode("utf-8"))
                message = req["message"]

                ai = model.generate_content(message)
                response_text = ai.text

                resp = json.dumps({"response": response_text}, ensure_ascii=False)
                conn.sendall(resp.encode("utf-8"))

            except Exception as e:
                error_json = json.dumps({"error": str(e)})
                conn.sendall(error_json.encode("utf-8"))

def start_server():
    print(f"AI Server running on {HOST}:{PORT}")

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind((HOST, PORT))
        s.listen()

        while True:
            conn, addr = s.accept()
            print("Client connected:", addr)
            handle_client(conn)

if __name__ == "__main__":
    start_server()
