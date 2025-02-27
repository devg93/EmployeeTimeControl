

export interface Userrepositoryinterface extends AuthServiceInterface {

    register(body: any): Promise<any>;
    updateUser(id: number, updateRegistracionDto: any): Promise<any>;
    remove(id: number): Promise<any>;
    delete(email: string): Promise<any>;

}

export interface AuthServiceInterface {

    getProfileById(id: string): Promise<any>;
    getProfileByEmail(useremail: string): Promise<any>;
    login(email: string, password: string): Promise<any>;
    validateUser(email: string, password: string): Promise<any>
    findAll(): Promise<any>;

}

export interface UserInterface extends Userrepositoryinterface{}