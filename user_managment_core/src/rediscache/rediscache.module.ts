import { Module, Global } from '@nestjs/common';

import Redis from 'ioredis';
import { RedisRepository } from './repository/rediscache.RedisRepository';

import { ReadService } from './services/read.redis.service';
import { WriteService } from './services/write.redis.service';


@Module({

  providers: [
    {
      
      provide: 'REDIS_CLIENT',
      useFactory: () => {
        return new Redis({
          host: process.env.REDIS_HOST || 'localhost',
          port: Number(process.env.REDIS_PORT) || 6379,
          password: process.env.REDIS_PASSWORD || undefined,
        });
      },
    },
    RedisRepository,

      //******************* */
        {
          provide: 'IredisReadService',
          useClass: ReadService
        },
     //*******************
     
         {
           provide: 'IredisWriteService',
           useClass: WriteService
         }
      //*******************
  ],
  exports: ['IredisWriteService','IredisReadService'],

})
export class RedisModule {}
