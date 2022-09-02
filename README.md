# UnityGoogleSheetsImporter - простой инструмент для импортирования гугл таблиц в ваш Unity проект.
*Read this in other languages: [Русский](https://github.com/AndreyBirchenko/UnityGoogleSheetsImporter/blob/master/README.md), English*

## Установка
[Скачать](https://github.com/AndreyBirchenko/UnityGoogleSheetsImporter/raw/master/AB_GSImporter_v2.9.22.unitypackage) unity packge файл и импортировать в проект.

## Начало работы
Откройте окно загрузки таблиц. Для этого нажмите **Tools -> GoogleSheetsImporter**.

![alt text](https://github.com/AndreyBirchenko/UnityGoogleSheetsImporter/blob/master/Images/photo_1.jpg)

Нажмите **Add** чтобы добавить новую таблицу.

![alt text](https://github.com/AndreyBirchenko/UnityGoogleSheetsImporter/blob/master/Images/photo_2.png)

В графу **Download url path:** вставьте [публичную](https://support.google.com/docs/answer/2494822?hl=en&co=GENIE.Platform%3DDesktop#zippy=) ссылку на гугл таблицу
(это та ссылка которая появляется после того как вы нажали кнопку "Поделиться")

В графу **Local path:** добавьте путь куда вы хотите сохранить таблицу
> **ВАЖНО!** Путь уже содержит в себе папку Assets. То есть если вы хотите сохранить таблицу по пути Assets/MyFolder, то достаточно написать только MyFolder. Если директории не нуществует, то она будет создана автоматически.

В графу **Name:** добавьте имя таблицы.

В граве **File format:** выберете нужный формат.

Нажмите **Download** и подождите пока таблица загрузится.

## Графа Selected
Если у вас много таблиц и вы не хотите скачивать заново все, вы можете снять галочку Selected с тех, которые не нуждаются в обновлении.
Те таблицы на которых стоит галочка Selected будут скачиваться заново каждый раз при нажатии кнопки Download.
