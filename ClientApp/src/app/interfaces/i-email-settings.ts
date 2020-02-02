export interface IEmailSettings {
    username: string;
    emailAddress: string;
    password: string;
    smtp: string;
    port: number;
    enableSsl: boolean;
    isHtmlMessage: boolean;
}
