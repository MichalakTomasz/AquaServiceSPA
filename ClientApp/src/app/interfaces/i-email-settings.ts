export interface IEmailSettings {
    username: string;
    password: string;
    emailAddress: string;
    smtp: string;
    port: number;
    enableSsl: boolean;
    isHtmlMessage: boolean;
}
