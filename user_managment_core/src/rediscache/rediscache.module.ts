import { Module, Global } from '@nestjs/common';

import Redis from 'ioredis';
import { RedisRepository } from './rediscache.RedisRepository';

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
    RedisRepository,
  ],
  exports: ['REDIS_CLIENT', RedisRepository],
})
export class RedisModule {}
