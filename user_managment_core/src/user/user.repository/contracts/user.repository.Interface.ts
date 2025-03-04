

export interface Userrepositoryinterface  {

//*****************RegistracionServiceMethods******************* */
    register(body: any): Promise<any>;
    updateUser(id: string, updateRegistracionDto: any): Promise<any>;
    deleteByemail(email: string): Promise<any>;

//*****************AuthServiceMethods******************* */
    getProfileById(id: string): Promise<any>;
    getProfileByEmail(useremail: string): Promise<any>;
    login(email: string, password: string): Promise<any>;
    validateUser(email: string, password: string): Promise<any>
    findAll(): Promise<any>;

}

//*****************exstends UserServiceMethods******************* */


export interface IuserInterface extends Userrepositoryinterface{}