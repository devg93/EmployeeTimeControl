import { Inject, Injectable } from '@nestjs/common';

import { IredisReadService, IredisWriteService } from 'src/rediscache/contracrs';
import { IuserReadInterface, Userrepositoryinterface } from '../user.repository/contracts/user.repository.Interface';
import * as bcrypt from 'bcryptjs';

@Injectable()
export class UserReadService implements Partial<IuserReadInterface> {

    constructor(@Inject('Userrepositoryinterface') private readonly userrepositoryinterface: Userrepositoryinterface,
        @Inject('IredisReadService') private readonly redisReadService: IredisReadService,
        @Inject('IredisWriteService') private readonly redisWriteService: IredisWriteService) { }


    async validateUser(email: string, password: string): Promise<any> {
        const res = await this.redisReadService.redislogin(email, password);
        if (!res) return await this.userrepositoryinterface.validateUser(email, password);
        return res;
    }


    async getProfileById(id: string): Promise<any> {

        const res = await this.redisReadService.redisfindUser(id);
        if (!res) {

            const resSql = await this.userrepositoryinterface.getProfileById(id);

            if (resSql) await this.redisWriteService.redisregisterUser(resSql);

            return resSql;
        }
        return res;
    }

    async getProfileByEmail(useremail: string): Promise<any> {
        const res = await this.redisReadService.redisfindUser(useremail);
        if (!res) {
            const resSql = await this.userrepositoryinterface.getProfileByEmail(useremail);
            if (resSql) await this.redisWriteService.redisregisterUser(resSql);
            return resSql;
        }
        return res;
    }


    async login(email: string, password: string): Promise<any> {
        // const res = await this.redisReadService.redislogin(email, password);
        // console.log(res)
        // if (!res) return "redis miss users"
        //      if (res && (await bcrypt.compare(password, res.password))) {
        //        const { passWord, ...result } = res;
       
        //        return result;
        //      }
          const token =  await this.userrepositoryinterface.validateUser(email, password);
        return token;
    }


    async redisfindAll(): Promise<any> {
        const res = await this.redisReadService.redisfindAll();
        if (!res) return await this.userrepositoryinterface.findAll();
        return res;
    }



}
