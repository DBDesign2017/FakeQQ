CREATE DATABASE JinNangIM_DB 

ON PRIMARY (                                    /*主文件*/
NAME = JinNangIM_Data,                          /*数据库主文件逻辑名*/
FILENAME = 'C:\JN_DataBase\JinNangIM_Data.mdf', /*数据库主文件物理名称*/
SIZE = 5MB,                                     /*数据库初始容量大小*/
MAXSIZE = UNLIMITED,                            /*数据库容量最大尺寸*/
FILEGROWTH = 10%                                /*数据库容量增长率*/
)

LOG ON (                                       /*事务日志文件*/
NAME = JinNangIM_Log,                          /*事务日志逻辑名*/
FILENAME = 'C:\JN_DataBase\JinNangIM_Log.mdf', /*事务日志文件物理名称*/
SIZE = 2MB,                                    /*数据库初始容量大小*/
MAXSIZE = UNLIMITED,                           /*数据库容量最大尺寸*/
FILEGROWTH = 10%                               /*数据库容量增长率*/
)

GO

USE JinNangIM_DB
GO

CREATE TABLE Users
( UserID int primary key,      --默认限定为10位--
Nickname varchar(20),
[Password] varchar(16) not null,
Sex char,
Birthday varchar(12),
[Location] varchar(20),
Tel varchar(11),
Email varchar(20),
[Signature] varchar(20),
Question varchar(30),
Answer varchar(30),
SignDate varchar(12),
)

CREATE TABLE Administrator
(AdministratorID int primary key,
[Password] varchar(16) not null,
Nickname varchar(20),
)

CREATE TABLE Friends
(ID int primary key,
FriendID int not null,
)

CREATE TABLE Logs
(AdministratorId int primary key,
Class varchar(20),
Date varchar(12),
Operation varchar(50),
)

CREATE TABLE ChatHistory
(UserIDa int,
UserIDb int,
[Date] varchar(12),
[Message] varchar(300),
)

CREATE TABLE Broadcast
([Date] varchar(12) primary key,
AdministratorID int,
Notice varchar(500),
)

CREATE TABLE Datas
([Date] varchar(12) primary key,
[Online] int,
Member int,
)

GO