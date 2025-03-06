import { Inject, Injectable, UnauthorizedException } from '@nestjs/common';
import * as bcrypt from 'bcrypt';

import { IredisReadService, IredisWriteService } from 'src/rediscache/contracrs';
import { IuserReadService, IuserQeuryRepository } from '../libs/contracts/user.repository.Interface';
import { JwtService } from '@nestjs/jwt';


@Injectable()
export class UserReadService implements IuserReadService {

    constructor(@Inject('IuserQeuryRepository') private readonly userrepositoryinterface: IuserQeuryRepository,
        @Inject('IredisReadService') private readonly redisReadService: IredisReadService,
        @Inject('IredisWriteService') private readonly redisWriteService: IredisWriteService,
        private readonly jwtService: JwtService) { }


    async getProfileByIdService(id: string): Promise<any> {
        const res = await this.redisReadService.redisfindUser(id);
        if (!res) {

            const resSql = await this.userrepositoryinterface.getProfileById(id);

            if (resSql) await this.redisWriteService.redisregisterUser(resSql);

            return resSql;
        }
        return res;
    }

    async getProfileByEmailService(useremail: string): Promise<any> {

        console.log('getProfileByEmailService',useremail)
        const res = await this.redisReadService.redisfindUser(useremail);
    //   console.log(res)
        // if (!res) {
        //     const resSql = await this.userrepositoryinterface.getProfileByEmail(useremail);
        //     if (resSql) await this.redisWriteService.redisregisterUser(resSql);
        //     return resSql;
        // }
        return res;
    }

 

    async loginService(body: any): Promise<any> {
        const user = await this.redisReadService.redisfindUser(body.email);
    console.log(user)
        if (!user) {
            throw new UnauthorizedException('User not found.');
        }
    
       
        // const passwordValid = await bcrypt.compare(body.password, user.password);
        
        // if (!passwordValid) {
        //     throw new UnauthorizedException('Wrong password.');
        // }
    
       
        const payload = {
            userName: body.userName, 
                email: body.email,
                password: body.password,
                iPadrres: body.iPadrres,
                deviceName: body.deviceName,
        };
    
        return {
            token: this.jwtService.sign(payload),
        };
    }
    

    async validateUserService(email: string, password: string): Promise<any> {

        // const user = await this.redisReadService.redislogin(email, password);

        // return await this.userrepositoryinterface.(user.email, user.password);
    }

    async findAllService(body:any): Promise<any> {
        // const resUser = await this.redisReadService.redislogin(body.email, body.password);
        // if (!resUser) {
        //     const user = await this.userrepositoryinterface.getProfileByEmail(body.email);
        //     const Restoken = await this.userrepositoryinterface.login(user);

        //     return Restoken;
        // }
        // const Restoken = await this.userrepositoryinterface.login(resUser);
        // return Restoken;
        const res = await this.redisReadService.redisfindAll();
        if (!res) return await this.userrepositoryinterface.findAll();
        return res;
    }



}
