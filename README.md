# VestanTestPlugin

## Назначение

Простая библиотека на C# для взаимодействия с REST API [JSONPlaceholder](https://jsonplaceholder.typicode.com), реализованная в рамках [тестового задания](https://docs.google.com/document/d/1CsjCDZMP0KQavQaTCznZ3XNIIIXT-B0-KOQHCxSutcI/edit?tab=t.0).

Проект оформлен с учётом возможности командной разработки, сопровождения и расширения функциональности.

---

## Реализованные задачи

✅ Разработка библиотеки на чистом C# для взаимодействия с JSONPlaceholder API:  
- Получение списка постов (`GET /posts`)  
- Добавление нового поста (`POST /posts`)  
- Редактирование поста (`PUT /posts/{id}`)  
- Удаление поста (`DELETE /posts/{id}`)  

✅ Оформление кода с прицелом на расширяемость:  
- Разделение по логическим модулям  
- Внедрение зависимостей через фабрику  
- Реализация команд в отдельной структуре (`Command Pattern`)

  ## Технические детали

- Каждая операция оформлена в виде команды (`IPostCommand`)
- Внедрение зависимостей выполнено вручную через `LocatorFactory`
- Используется `System.Net.Http` для выполнения HTTP-запросов

[Unity Project](https://github.com/Cust0mize/VestanTestUnityProject)
