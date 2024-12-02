![logo-social-small](https://github.com/user-attachments/assets/7d8060c4-122d-4305-be4e-24f9089fa860)

## What is Palleto?

Palleto is an auxiliary service for pallet management and product labeling for food production.

## Structure
There are several clients to work with the system, divided into different devices with different functionality:
- [Admin](Src/Apps/Web/Pl.Admin.Client) - Web application for administration (device registration, entity browsing)
- [Desktop](Src/Apps/Desktop/Pl.Desktop.Client) - Windows application for product labeling and pallet management
- [Mobile](Src/Apps/Mobile/Pl.Mobile.Client) - Android application for pallet managment (with barcode reading feature)
- [Tablet](Src/Apps/Tablet/Pl.Tablet.Client) - Android application for pallet managment

## Features
- Working with Zpl and Tsc printers via TCP connection
- Managing print templates using Redis
- Configuring barcodes
- Information exchange with 1C
- User authorization via Keycloak with user database connection via Active Directory
- Barcode reading via barcode reader / camera
- Working with MassaK scales via usb
- Production performance analytics

## License
The Palleto project is licensed under the [MIT License](LICENSE.md). Feel free to modify and distribute this code as per the terms of the license.
