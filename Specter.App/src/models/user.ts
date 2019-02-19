export class User {
    email: string;
    firstName: string;
    lastName: string;
    darkMode: boolean;
    token?: string;
}

export class UserCreate {
    email: string;
    firstName: string;
    lastName: string;
    password: string;
}
