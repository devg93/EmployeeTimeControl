import { OnEvent } from '@nestjs/event-emitter';
import { Injectable, OnModuleInit } from '@nestjs/common';
import { MongoClient } from 'mongodb';
import Redis from 'ioredis';

@Injectable()
export class UserEventsSubscriber implements OnModuleInit {
  private mongoClient: MongoClient;
  private redisClient: Redis;

  onModuleInit() {
   
    // this.mongoClient = new MongoClient('mongodb://localhost:27017');
    // this.mongoClient.connect()
    //   .then(() => console.log('Connected to MongoDB'))
    //   .catch(err => console.error('MongoDB connection error:', err));
    
 
    this.redisClient = new Redis('redis://localhost:6379');
  }

  @OnEvent('user.created')
  async handleUserCreatedEvent(payload: any) {
  
    const{userName,iPadrres,email}=payload;
    try {
      const db = this.mongoClient.db('users');
      const collection = db.collection('userLogs');
      await collection.insertOne(payload );
      console.log('User created event stored in MongoDB:', payload);
    } catch (error) {
      console.error('Error storing event in MongoDB:', error);
    }

  
    // try {
    //   await this.redisClient.lpush('user:created:events', JSON.stringify(payload));
    //   console.log('User created event stored in Redis:', payload);
    // } catch (error) {
    //   console.error('Error storing event in Redis:', error);
    // }
  }
}
