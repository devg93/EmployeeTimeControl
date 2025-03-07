import { Inject, Injectable, Scope } from '@nestjs/common';

import { RedisRepository } from 'src/rediscache/repository/rediscache.RedisRepository';
import { CreateRegistracionDto } from '../../user/dto/create-registracion.dto';
import { IredisWriteService } from '../contracrs';
import { OnEvent } from '@nestjs/event-emitter';



@Injectable()
export class WriteService implements IredisWriteService {

    constructor(@Inject() private readonly redisService: RedisRepository) {
     }

 

  @OnEvent('user.created.event')
    async redisregisterUser(userEntity: CreateRegistracionDto): Promise<any> {
       
        // const res = await this.redisService.findOneRedis(userEntity.email);
        // console.log(res);
        //  const hashedPassword = await bcrypt.hash(userEntity.password, 10);
        // console.log(userEntity  );

        // if (res) return "user arledy registred"
         return await this.redisService.registerUserRedis(userEntity);
    }

    @OnEvent('user.update.event')
    async redisupdateUser(email:string,userEntity: CreateRegistracionDto): Promise<any> {
        // const res = await this.findUser(userEntity.email);
        // if (res) return "user arledy registred"
        return await this.redisService.updateUser(email,userEntity);
    }


    @OnEvent('user.delete.event')
    async redisdelete(email: string) {
        return await this.redisService.deleteUser(email);
    }


}
