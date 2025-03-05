export interface IredisReadService {

    redisfindUser(email: string): Promise<any>;
    redislogin(email: string, password: string): Promise<any>;
     redisfindAll() :Promise<any>;

}

export interface IredisWriteService {

    redisregisterUser(userEntity: any): Promise<any>;
    redisupdateUser(email: string, userEntity: any): Promise<any>;
    redisdelete(email: string): Promise<any>;
   
}