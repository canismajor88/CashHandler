# CashHandler

## Cash Handler API

The custom-built API handles all the business logic for the application. We followed the model view controller (MVC) design pattern where our view will be our Angular app, and our controllers and models are held in our API. The API will be a layer of abstraction which will facilitate interactions between the user interface and the database. It will also require authentication for access to the database, and will validate inputs where necessary. The API also consumes Microsoft's Entity Framework Core. This abstracts the communication between the API and the SQL database in order to build our endpoints in a more efficient and safe manner.

## Cash Handler UI

[Link to Github repo for the UI project](https://github.com/canismajor88/CashHandlerUI)

The user interface is the bridge between the user and the business logic. The user interface is built with Angular 13 and Bootstrap 5 to provide a responsive and intuitive foundation upon which users can access the app from either a desktop or mobile device.

The database is currently hosted on Azure, though the API and Angular project have not yet been deployed. Please contact Zach (zjuvz6@mail.umkc.edu) if you are running the API locally and need access to the database, as we will have to add a firewall exception for your local machine.

## References:

Regex:

- [Geeks For Geeks](https://www.geeksforgeeks.org/how-to-validate-a-password-using-regular-expressions-in-java/)
- [Expert Code Blog](https://expertcodeblog.wordpress.com/2018/05/21/typescript-regexp/)
- [I Hate Regex](https://ihateregex.io/expr/password/)
- [YouTube](https://www.youtube.com/watch?v=V8GVKAVkTVc)
- [Cloud Hadoop](https://www.cloudhadoop.com/angular-number-validation-example/)

Authorization and Authentication Series:

- [YouTube](https://www.youtube.com/watch?v=633CJ1V01lg)

Email API:

- [YouTube](https://www.youtube.com/watch?v=rMAAp5g7-1Q)

UI design:

- [Bootstrap Alerts](https://getbootstrap.com/docs/4.3/components/alerts/)
- [Bootstrap Tables](https://getbootstrap.com/docs/5.0/content/tables/)
- [Angular Forms](https://angular.io/guide/reactive-forms#validating-form-input)
- [YouTube](https://www.youtube.com/watch?v=akXfF066MY0)
- [Sitepoint](https://www.sitepoint.com/understanding-and-using-rem-units-in-css/)
- [CSS Gradient](https://cssgradient.io/)
- [Adit Yatyagi](https://adityatyagi.com/index.php/2019/12/07/importing-fonts-in-angular/)
- [YouTube](https://www.youtube.com/watch?v=bs1dNMLFWt8)

Images and Style:

- [Many Pixels](https://www.manypixels.co/gallery)
- [Unsplash](https://unsplash.com/)
- [Getwaves](https://getwaves.io/https://newbedev.com/how-to-refresh-a-page-programmatically-in-angular-code-example)
