import google.generativeai as genai

genai.configure(api_key="AIzaSyD82mvQGIU8BHcghr2i9I4Zn9NmSSzbzqE")

model = genai.GenerativeModel("models/gemini-2.5-flash")

response = model.generate_content("придумай тему для дипломної роботи по спеціальності ІПЗ")
print(response.text)


