CREATE DATABASE AgendaContactos
go
USE AgendaContactos
go
Create table Contactos(
Id_Contacto int primary key identity(1,1),
Nombre nVarchar(100) not null,
Apellido nvarchar (100) not null,
Telefono nvarchar(20) not null,
Email nvarchar(100) not null,
Direccion nvarchar(100) not null
);
