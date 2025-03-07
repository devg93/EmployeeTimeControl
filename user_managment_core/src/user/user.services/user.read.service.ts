import { Inject, Injectable, UnauthorizedException } from '@nestjs/common';
import * as bcrypt from 'bcrypt';

import { IredisReadService, IredisWriteService } from 'src/rediscache/contracrs';
import { IuserReadService, IuserQeuryRepository } from '../contracts/user.repository.Interface';
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

        const res = await this.redisReadService.redisfindUser(useremail);
        
        if (!res) {
            const resSql = await this.userrepositoryinterface.getProfileByEmail(useremail);
            if (resSql) await this.redisWriteService.redisregisterUser(resSql);
            return resSql;
        }
        return res;
  
    }

 

    async loginService(body: any): Promise<any> {
        const user = await this.redisReadService.redisfindUser(body.email);
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
     
        const res = await this.redisReadService.redisfindAll();
        if (!res) return await this.userrepositoryinterface.findAll();
        return res;
    }



}
