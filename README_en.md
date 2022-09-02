# UnityGoogleSheetsImporter - простой инструмент для импортирования гугл таблиц в ваш Unity проект.
*Read this in other languages: [Русский](https://github.com/AndreyBirchenko/UnityGoogleSheetsImporter/blob/master/README.md), English*

## Установка
[Download ](https://github.com/AndreyBirchenko/UnityGoogleSheetsImporter/raw/master/AB_GSImporter_v2.9.22.unitypackage) the unity package file and import it into the project.

## Начало работы
Open the table loading window. To do this, click **Tools -> GoogleSheetsImporter**.

![alt text](https://github.com/AndreyBirchenko/UnityGoogleSheetsImporter/blob/master/Images/photo_1.jpg)

Click **Add** to add a new table.

![alt text](https://github.com/AndreyBirchenko/UnityGoogleSheetsImporter/blob/master/Images/photo_2.png)

In the column **Download url path:** insert [public](https://support.google.com/docs/answer/2494822 ?hl=en&co=GENIE.Platform%3DDesktop#zippy=) link to google table
(this is the link that appears after you click the "Share" button)

In the column **Local path:** add the path where you want to save the table
> **IMPORTANT!** The path already contains the Assets folder. That is, if you want to save the table along the Assets/MyFolder path, then it is enough to write only MyFolder. If the directory does not exist, it will be created automatically.

In the column **Name:** add a table name.

In the column **File format:** select the desired format.

Click **Download** and wait for the table to load.

## Selected toggle
If you have a lot of tables and you don't want to download everything again, you can uncheck Selected from those that don't need updating.
Those tables with the Selected check mark will be downloaded again every time you click the Download button.
