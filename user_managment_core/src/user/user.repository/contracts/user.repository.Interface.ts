

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

export interface IuserWriteInterface extends Userrepositoryinterface {
    
    // registerService(body: any): Promise<any>;
    // updateUserService(id: string, updateRegistracionDto: any): Promise<any>;
    // deleteByemailService(email: string): Promise<any>;
}

export interface IuserReadInterface extends Userrepositoryinterface {
    // getProfileByIdService(id: string): Promise<any>;
    // getProfileByEmailService(useremail: string): Promise<any>;
    // loginService(email: string, password: string): Promise<any>;
    // validateUserService(email: string, password: string): Promise<any>
    // findAllService(): Promise<any>;
   
}
