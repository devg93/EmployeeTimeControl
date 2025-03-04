export interface IredisServiceInterface {

    redisfindUser(email: string): Promise<any>;
    redisregisterUser(userEntity: any): Promise<any>;
    redisupdateUser(email: string, userEntity: any): Promise<any>;
    redislogin(email: string, password: string): Promise<any>;
    redisdelete(email: string): Promise<any>;
    redisfindAll(): Promise<any>;
}