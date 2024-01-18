create schema if not exists email_logging;

comment on schema email_logging is 'Схема для логирования отправки email сообщений.';

create table if not exists email_logging.message
(
    id serial primary key,
    subject varchar(64) not null,
    body text not null,
    recipients text NOT NULL,
    send_time timestamptz not null,
    send_result int not null,
    failed_message text
);

COMMENT ON TABLE email_logging.message IS 'Таблица для логирования результатов отправки email сообщений.';

COMMENT ON COLUMN email_logging.message.id IS 'Уникальный идентификатор записи в логе.';
COMMENT ON COLUMN email_logging.message.subject IS 'Тема письма.';
COMMENT ON COLUMN email_logging.message.body IS 'Тело письма.';
COMMENT ON COLUMN email_logging.message.send_time IS 'Время отправки письма.';
COMMENT ON COLUMN email_logging.message.send_result IS 'Статус отправки письма.';
COMMENT ON COLUMN email_logging.message.failed_message IS 'Сообщение об ошибке при отправке, если есть.';
