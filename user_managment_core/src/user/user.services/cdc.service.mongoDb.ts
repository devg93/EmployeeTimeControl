import { OnEvent } from '@nestjs/event-emitter';
import { Injectable } from '@nestjs/common';
import { MongoClient } from 'mongodb';

@Injectable()
export class UserEventsSubscriber {
    private mongoClient: MongoClient;

    constructor() {
        this.mongoClient = new MongoClient('mongodb://localhost:27017');

    }

    @OnEvent('user.created')
    async handleUserCreatedEvent(payload: any) {
     
        await this.mongoClient.connect();
        const db = this.mongoClient.db('users');
        const collection = db.collection('userLogs');
               await collection.insertOne({
                ...payload
            })

        console.log('User created event processed and stored in MongoDB', payload);
    }
}
