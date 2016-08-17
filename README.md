# CMOV-Stock-Exchange

### Repository for the second assignment of the [Mobile Computing](https://sigarra.up.pt/feup/pt/ucurr_geral.ficha_uc_view?pv_ocorrencia_id=384972) course of the Master's in Informatics of [FEUP](https://sigarra.up.pt/feup/pt/web_page.inicial)

This project consisted in creating a mobile app for the Windows Universal platform and a server in a platform of the students choosing to feed information to the application.

The app developed allows for the user to choose stocks he wants to keep track of and displaying updated information about their value. The app should also display a graphic of the evolution of the stock's value, allowing the user to choose the time interval to display. The app also uses the Push Notification system to notify the user when some of the stock's chosen surpasses a defined threshold.

The server was developed in Node.js and is responsible for feeding all the information necessary to keep the app running such as:

* Creating user
* Set thresholds for all the stocks
* Limit the portfolio max and minimum values

More info can be found on the [assignment pdf](https://github.com/CMatias/CMOV-Stock-Exchange/blob/master/assignment.pdf).
