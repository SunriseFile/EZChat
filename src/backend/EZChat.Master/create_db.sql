CREATE DATABASE ezchat;

CREATE TABLE main.users
(
  id                  BIGSERIAL    NOT NULL
    CONSTRAINT users_pk
      PRIMARY KEY,
  username            VARCHAR(256) NOT NULL,
  normalized_username VARCHAR(256) NOT NULL,
  display_name        VARCHAR(256) NOT NULL,
  password_hash       TEXT         NOT NULL
);

CREATE UNIQUE INDEX users_id_uindex
  ON main.users (id);

CREATE UNIQUE INDEX users_normalized_username_uindex
  ON main.users (normalized_username);

CREATE UNIQUE INDEX users_username_uindex
  ON main.users (username);
