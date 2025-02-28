

export interface Userrepositoryinterface  {

//*****************RegistracionServiceMethods******************* */
    register(body: any): Promise<any>;
    updateUser(id: string, updateRegistracionDto: any): Promise<any>;
    remove(id: string): Promise<any>;
    delete(email: string): Promise<any>;

//*****************AuthServiceMethods******************* */
    getProfileById(id: string): Promise<any>;
    getProfileByEmail(useremail: string): Promise<any>;
    login(email: string, password: string): Promise<any>;
    validateUser(email: string, password: string): Promise<any>
    findAll(): Promise<any>;

}

//*****************exstends UserServiceMethods******************* */
export interface UserInterface extends Userrepositoryinterface{}