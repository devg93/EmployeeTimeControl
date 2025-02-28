import { Inject, Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { RedisRepository } from 'src/rediscache/rediscache.RedisRepository';
import { UserInterface, Userrepositoryinterface } from '../user.repository/contracts/user.repository.Interface';
import { RedisService } from './redis.service';

@Injectable()
export class UserService implements UserInterface {

    constructor(@Inject('Userrepositoryinterface') private readonly userrepositoryinterface: Userrepositoryinterface,
        private readonly redisService: RedisService) { }

        
 //****************************************************************************
    async updateUser(id: string, updateRegistracionDto: any): Promise<any> {

        const res = await this.userrepositoryinterface.updateUser(id, updateRegistracionDto);
        if (res) {
            await this.redisService.redisupdateUser(id, updateRegistracionDto);
        }
        return res;

    }
 //****************************************************************************
    async remove(id: string): Promise<any> {
        const res = await this.userrepositoryinterface.remove(id);
        if (res) await this.redisService.redisdelete(id);

    }
 //****************************************************************************
    async delete(email: string): Promise<any> {
        const res = await this.userrepositoryinterface.delete(email);
        if (res) await this.redisService.redisdelete(email);
    }
 //****************************************************************************
    async register(body: any): Promise<any> {
        const res = await this.userrepositoryinterface.register(body);
        if (res) return await this.redisService.redisregisterUser(body);
     
    }

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
