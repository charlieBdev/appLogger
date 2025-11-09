-- connect to database
\c app1_dev

-- create table with 'user' column
CREATE TABLE logging (
    id SERIAL PRIMARY KEY,
    timestamp TIMESTAMP NOT NULL,
    level TEXT NOT NULL,
    message TEXT NOT NULL,
    username TEXT NOT NULL
);

-- insert data
INSERT INTO logging (timestamp, level, message, username) VALUES
    (NOW(), 'INFO', 'App1_dev started', 'alice@example.com'),
    (NOW(), 'ERROR', 'App1_dev error occurred', 'bob@example.com');

\c app1_prod

CREATE TABLE logging (
    id SERIAL PRIMARY KEY,
    timestamp TIMESTAMP NOT NULL,
    level TEXT NOT NULL,
    message TEXT NOT NULL,
    username TEXT NOT NULL
);

INSERT INTO logging (timestamp, level, message, username) VALUES
    (NOW(), 'INFO', 'App1_prod started', 'charlie@example.com'),
    (NOW(), 'WARNING', 'App1_prod warning', 'dave@example.com'),
    (NOW(), 'ERROR', 'App1_prod failure', 'eve@example.com');

\c app2_test

CREATE TABLE logging (
    id SERIAL PRIMARY KEY,
    timestamp TIMESTAMP NOT NULL,
    level TEXT NOT NULL,
    message TEXT NOT NULL,
    username TEXT NOT NULL
);

INSERT INTO logging (timestamp, level, message, username) VALUES
    (NOW(), 'INFO', 'App2_test init', 'frank@example.com'),
    (NOW(), 'INFO', 'App2_test running', 'grace@example.com'),
    (NOW(), 'WARNING', 'App2_test warning', 'heidi@example.com'),
    (NOW(), 'ERROR', 'App2_test error', 'ivan@example.com');

\c app2_prod

CREATE TABLE logging (
    id SERIAL PRIMARY KEY,
    timestamp TIMESTAMP NOT NULL,
    level TEXT NOT NULL,
    message TEXT NOT NULL,
    username TEXT NOT NULL
);

INSERT INTO logging (timestamp, level, message, username) VALUES
    (NOW(), 'INFO', 'App2_prod started', 'judy@example.com'),
    (NOW(), 'ERROR', 'App2_prod failed', 'mallory@example.com');

\c app3_staging

CREATE TABLE logging (
    id SERIAL PRIMARY KEY,
    timestamp TIMESTAMP NOT NULL,
    level TEXT NOT NULL,
    message TEXT NOT NULL,
    username TEXT NOT NULL
);

INSERT INTO logging (timestamp, level, message, username) VALUES
    (NOW(), 'INFO', 'App3_staging started', 'nancy@example.com'),
    (NOW(), 'INFO', 'App3_staging running', 'oliver@example.com'),
    (NOW(), 'WARNING', 'App3_staging warning', 'peggy@example.com'),
    (NOW(), 'ERROR', 'App3_staging error', 'quentin@example.com'),
    (NOW(), 'INFO', 'App3_staging completed', 'rachel@example.com');

\c app3_prod

CREATE TABLE logging (
    id SERIAL PRIMARY KEY,
    timestamp TIMESTAMP NOT NULL,
    level TEXT NOT NULL,
    message TEXT NOT NULL,
    username TEXT NOT NULL
);

INSERT INTO logging (timestamp, level, message, username) VALUES
    (NOW(), 'INFO', 'App3_prod started', 'sam@example.com'),
    (NOW(), 'ERROR', 'App3_prod failed', 'trudy@example.com'),
    (NOW(), 'WARNING', 'App3_prod warning', 'victor@example.com');

