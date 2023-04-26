use master

create database registro_mvc_1tabla

use registro_mvc_1tabla

create table usuarios(
idUsuario int identity(1,1) primary key,
nombre varchar(50),
edad int,
correo varchar(50)
);

create procedure sp_registrar
@nombre varchar(50),
@edad int,
@correo varchar(50)
as begin
insert into usuarios values (@nombre, @edad, @correo);
end


create procedure sp_usuarios
as begin
select * from usuarios;
end
