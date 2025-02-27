import { Inject, Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { RedisRepository } from 'src/rediscache/rediscache.RedisRepository';
import { CreateRegistracionDto } from '../dto/create-registracion.dto';

@Injectable()
export class RedisService {

    constructor(@Inject() private readonly redisService: RedisRepository) { }


    async findUser(email: string): Promise<any> {

        return await this.redisService.findOneRedis(email);

    }


    async registerUser(userEntity: CreateRegistracionDto): Promise<any> {
        const res = await this.findUser(userEntity.email);
         const hashedPassword = await bcrypt.hash(userEntity.password, 10);
        if (res) return "user arledy registred"
        return await this.redisService.registerUserRedis(userEntity);
    }
  
    async updateUser(email:string,userEntity: CreateRegistracionDto): Promise<any> {
        // const res = await this.findUser(userEntity.email);
        // if (res) return "user arledy registred"
        return await this.redisService.updateUser(email,userEntity);
    }


    async login(email: string, password: string) {
        return await this.redisService.loginRedis(email, password);
    }


    async delete(email: string) {
        return await this.redisService.deleteUser(email);
    }



    async findAll(email: string) {
        await this.redisService.findAllEntities()

    }


}
