export class User {
    email: string;
    firstName: string;
    lastName: string;
    darkMode: boolean;
    token?: string;
}

export class UserCreate {
    email: string;
    name: string;
    password: string;
}
