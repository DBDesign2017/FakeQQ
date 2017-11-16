CREATE DATABASE JinNangIM_DB 

ON PRIMARY (                                    /*���ļ�*/
NAME = JinNangIM_Data,                          /*���ݿ����ļ��߼���*/
FILENAME = 'C:\JN_DataBase\JinNangIM_Data.mdf', /*���ݿ����ļ���������*/
SIZE = 5MB,                                     /*���ݿ��ʼ������С*/
MAXSIZE = UNLIMITED,                            /*���ݿ��������ߴ�*/
FILEGROWTH = 10%                                /*���ݿ�����������*/
)

LOG ON (                                       /*������־�ļ�*/
NAME = JinNangIM_Log,                          /*������־�߼���*/
FILENAME = 'C:\JN_DataBase\JinNangIM_Log.mdf', /*������־�ļ���������*/
SIZE = 2MB,                                    /*���ݿ��ʼ������С*/
MAXSIZE = UNLIMITED,                           /*���ݿ��������ߴ�*/
FILEGROWTH = 10%                               /*���ݿ�����������*/
)

GO

USE JinNangIM_DB
GO

CREATE TABLE Users
( UserID int primary key,      --Ĭ���޶�Ϊ10λ--
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