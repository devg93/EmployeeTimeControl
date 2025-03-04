import { Inject, Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { RedisRepository } from 'src/rediscache/rediscache.RedisRepository';
import { CreateRegistracionDto } from '../user/dto/create-registracion.dto';
import { IredisServiceInterface } from './contracrs';

@Injectable()
export class ReadService implements Partial<IredisServiceInterface> {

    constructor(@Inject() private readonly redisService: RedisRepository) { }


    async redisfindUser(email: string): Promise<any> {

        return await this.redisService.findOneRedis(email);

    }


    async redisregisterUser(userEntity: CreateRegistracionDto): Promise<any> {
        const res = await this.redisfindUser(userEntity.email);
         const hashedPassword = await bcrypt.hash(userEntity.password, 10);
        if (res) return "user arledy registred"
        return await this.redisService.registerUserRedis(userEntity);
    }
  
    async redisupdateUser(email:string,userEntity: CreateRegistracionDto): Promise<any> {
        // const res = await this.findUser(userEntity.email);
        // if (res) return "user arledy registred"
        return await this.redisService.updateUser(email,userEntity);
    }


    async redislogin(email: string, password: string) {
        return await this.redisService.loginRedis(email, password);
    }


    async redisdelete(email: string) {
        return await this.redisService.deleteUser(email);
    }



    async redisfindAll() {
     return   await this.redisService.findAllEntities()

    }


}
