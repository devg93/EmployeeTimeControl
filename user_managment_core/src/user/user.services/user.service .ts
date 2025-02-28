import { Inject, Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { RedisRepository } from 'src/rediscache/rediscache.RedisRepository';
import { UserInterface, Userrepositoryinterface } from '../user.repository/contracts/user.repository.Interface';
import { RedisService } from './redis.service';

@Injectable()
export class UserService implements UserInterface {

    constructor(@Inject('Userrepositoryinterface') private readonly userrepositoryinterface: Userrepositoryinterface,
        private readonly redisService: RedisService) { }

    async updateUser(id: string, updateRegistracionDto: any): Promise<any> {
        await this.userrepositoryinterface.updateUser(id, updateRegistracionDto);
        await this.redisService.redisupdateUser(id, updateRegistracionDto);
        throw new Error('Method not implemented.');
    }

    async remove(id: string): Promise<any> {
        await this.userrepositoryinterface.remove(id);
        await this.redisService.redisdelete(id);
    }

    async delete(email: string): Promise<any> {
        await this.userrepositoryinterface.delete(email);
        await this.redisService.redisdelete(email);
    }

    async register(body: any): Promise<any> {
        await this.userrepositoryinterface.register(body);
        await this.redisService.redisregisterUser(body);
        throw new Error('Method not implemented.');
    }


    async getProfileById(id: string): Promise<any> {
        await this.userrepositoryinterface.getProfileById(id);
        await this.redisService.redisfindUser(id);
        throw new Error('Method not implemented.');
    }
    async getProfileByEmail(useremail: string): Promise<any> {
        await this.userrepositoryinterface.getProfileByEmail(useremail);
        await this.redisService.redisfindUser(useremail);
        throw new Error('Method not implemented.');
    }
    async login(email: string, password: string): Promise<any> {
        await this.redisService.redislogin(email, password);
        await this.userrepositoryinterface.login(email, password);

        throw new Error('Method not implemented.');
    }
    async validateUser(email: string, password: string): Promise<any> {
        await this.redisService.redislogin(email, password);
        await this.userrepositoryinterface.validateUser(email, password);
        throw new Error('Method not implemented.');
    }
    async findAll(): Promise<any> {
        await this.redisService.redisfindAll();
        await this.userrepositoryinterface.findAll();

        throw new Error('Method not implemented.');
    }




}
