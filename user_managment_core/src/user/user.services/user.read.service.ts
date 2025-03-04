import { Inject, Injectable } from '@nestjs/common';

import { IredisServiceInterface } from 'src/rediscache/contracrs';
import { Userrepositoryinterface } from '../user.repository/contracts/user.repository.Interface';


@Injectable()
export class UserReadService implements Partial<UserReadService>  {

    constructor(@Inject('Userrepositoryinterface') private readonly userrepositoryinterface: Userrepositoryinterface,
    @Inject('IredisServiceInterface')  private readonly redisService: IredisServiceInterface) { }

 //****************************************************************************
    async getProfileById(id: string): Promise<any> {

        const res = await this.redisService.redisfindUser(id);
        if (!res) { 

            const resSql = await this.userrepositoryinterface.getProfileById(id);

            if (resSql) await this.redisService.redisregisterUser(resSql);

            return resSql;
        }
        return res;
       
    }
 //****************************************************************************

    async getProfileByEmail(useremail: string): Promise<any> {
        const res = await this.redisService.redisfindUser(useremail);
        if (!res)
             {
                const resSql = await this.userrepositoryinterface.getProfileByEmail(useremail);
                if (resSql) await this.redisService.redisregisterUser(resSql);
                return resSql;
            }
        return res;

    }

 //****************************************************************************
    async login(email: string, password: string): Promise<any> {
        const res = await this.redisService.redislogin(email, password);
        if (!res) return await this.userrepositoryinterface.login(email, password);
        return res;
    }

 //****************************************************************************

    async validateUser(email: string, password: string): Promise<any> {
        const res = await this.redisService.redislogin(email, password);
        if (!res) return await this.userrepositoryinterface.validateUser(email, password);
        return res;
    }

    //****************************************************************************

    async findAll(): Promise<any> {
        const res = await this.redisService.redisfindAll();
        if (!res) return await this.userrepositoryinterface.findAll();
        return res;

    }

 //****************************************************************************


}
