import { Module, Global } from '@nestjs/common';

import Redis from 'ioredis';
import { redisService } from './rediscache.service';

// @Global()
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
    redisService,
  ],
  exports: ['REDIS_CLIENT', redisService],
})
export class RedisModule {}
