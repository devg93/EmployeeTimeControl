import { Inject, Injectable } from '@nestjs/common';
import { OnEvent } from '@nestjs/event-emitter';
import Redis from 'ioredis';

@Injectable()
export class redisService {
    constructor(@Inject('REDIS_CLIENT') private redisClient: Redis) {}

      @OnEvent('user.created')
      async handleUserCreatedEvent(payload: any) {  
        try {
          await this.redisClient.lpush('user:created:events', JSON.stringify(payload));
          console.log('User created event stored in Redis:', payload);
        } catch (error) {
          console.error('Error storing event in Redis:', error);
        }
      }
}
