

export interface IuserCommandRepository  {

    register(body: any): Promise<any>;
    updateUser(id: string, updateRegistracionDto: any): Promise<any>;
    deleteByemail(email: string): Promise<any>;

}



export interface IuserQeuryRepository  {

        getProfileById(id: string): Promise<any>;
        getProfileByEmail(useremail: string): Promise<any>;
        findAll(): Promise<any>;
    
    }

//***************** UserServiceMethods******************* */

export interface IuserWriteService  {
    
    registerService(body: any): Promise<any>;
    updateUserService(id: string, updateRegistracionDto: any): Promise<any>;
    deleteByemailService(email: string): Promise<any>;
}

export interface IuserReadService  {
    getProfileByIdService(id: string): Promise<any>;
    getProfileByEmailService(useremail: string): Promise<any>;
    loginService(body:any): Promise<any>;
    validateUserService(email: string, password: string): Promise<any>
    findAllService(body:any): Promise<any>;
   
}
