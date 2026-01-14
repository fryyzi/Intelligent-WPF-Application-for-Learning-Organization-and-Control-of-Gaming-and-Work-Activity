
with open("F:\programing\Project\Wizzy\Wizzy\Commands\ToolsCommands.txt", "r", encoding="utf-8") as f:
    commands = [line.strip() for line in f.readlines()]

if any(cmd in "відкрий панель інструментів" for cmd in commands):
    print("Команда знайдена! Виконую дію...")
else:
    print("Команду не розпізнано")
