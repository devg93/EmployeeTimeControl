import { Inject, Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { RedisRepository } from 'src/rediscache/repository/rediscache.RedisRepository';
import { IredisReadService } from '../contracrs';


@Injectable()
export class ReadService implements IredisReadService {

    constructor(@Inject() private readonly redisService: RedisRepository) { }
   

    async redisfindUser(email: string): Promise<any> {

        return await this.redisService.findOneRedis(email);

    }

    async redislogin(email: string, password: string) {
        return await this.redisService.loginRedis(email, password);
    }


    async redisfindAll() {
     return   await this.redisService.findAllEntities()

    }


}
