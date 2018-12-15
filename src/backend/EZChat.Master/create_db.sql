CREATE DATABASE ezchat_db;

CREATE TABLE users
(
  "Id"                 BIGSERIAL    NOT NULL
    CONSTRAINT users_pk
      PRIMARY KEY,
  "UserName"           VARCHAR(256) NOT NULL,
  "NormalizedUserName" VARCHAR(256) NOT NULL,
  "FirstName"          VARCHAR(256) NOT NULL,
  "LastName"           VARCHAR(256),
  "PasswordHash"       TEXT         NOT NULL
);

CREATE UNIQUE INDEX users_id_uindex
  ON users ("Id");

CREATE UNIQUE INDEX users_normalizedusername_uindex
  ON users ("NormalizedUserName");

CREATE UNIQUE INDEX users_username_uindex
  ON users ("UserName");
