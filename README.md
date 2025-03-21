# PhoneBook ![C#](https://img.shields.io/badge/C%23-blue.svg)

## ✅ Given Requirements

 - [x] This is an application where you should record contacts with
       their phone numbers.
 - [x] Users should be able to Add, Delete, Update and Read from a
       database, using the console.
 - [x] You need to use Entity Framework, raw SQL isn't allowed.
 - [x] Your code should contain a base Contact class with AT LEAST
       {Id INT, Name STRING, Email STRING and Phone Number(STRING)}
 - [x] You should validate e-mails and phone numbers and let the
       user know what formats are expected
 - [x] You should use Code-First Approach, which means EF will create
       the database schema for you.
 - [x] You should use SQL Server, not SQLite

## 🚀 Features

  🔹 Spectre.Console SelectionPrompts are used to for easy user inputs,
  and they help reduce the validation required for the app.

  🔹 Send basic emails right from within the app using SMTP.

## 🔥 Challenges / Lessons Learned

  - I've never sent Emails or SMS from within an app. I was actually concerned
    I'd do it wrong and get a large bill from my carrier. (Fingers crossed I still don't)
    
  - I could have used an external service to add SMS from an external service's
    API. However, I couldn't get the question of reproducibility out of my
    head. This, on top of the fact that the SMS feature wasn't free to send
    made me reconsider adding an SMS feature to this app.

## 🌟 Things I Did Well

 ⚡ Good use of git feature-branches in workflow

 ## 📌 Areas to Improve

   🔍 How much should input-getting methods be separated from displaying methods? (oop conventions question)

## 📚 Resources Used

  🔗 [Simple SMTP email sending guide](https://www.codeproject.com/Articles/301836/Simple-SMTP-E-Mail-Sender-in-Csharp-Console-applic)
  
  🔗 [Spectre.Console Documentation](https://spectreconsole.net/prompts/selection)
  
  🔗[Regex CheatSheet](https://dev.to/ruppysuppy/the-regular-expression-regex-cheat-sheet-you-always-wanted-1c8h/comments)
  
  🔗[Regex Tester](https://regex101.com/)
  
  🔗ChatGPT and Google
  
