import { Module } from '@nestjs/common';
import * as dotenv from 'dotenv';
import { ConfigModule } from '@nestjs/config';
import {  userModule } from './user/user.module';
import { TypeOrmModule } from '@nestjs/typeorm';
import { DataSource } from 'typeorm';

dotenv.config();

@Module({
  imports: [
    ConfigModule.forRoot(),
    TypeOrmModule.forRoot({
      type: 'mysql',
      replication: {
        master: {
          host: process.env.MASTER_DB_HOST,  
          port: +process.env.DB_PORT || 3306,
          username: process.env.MASTER_DB_USERNAME || 'root',
          password: process.env.MASTER_DB_PASSWORD || 'password',
          database: process.env.MASTER_DB_NAME || 'userdatabase',
        },
        slaves: [
          {
            host: process.env.REPLICA_DB_HOST,  
            port: +process.env.DB_PORT || 3306,
            username: process.env.REPLICA_DB_USERNAME || 'replica_user',
            password: process.env.REPLICA_DB_PASSWORD || 'strongpassword',
            database: process.env.REPLICA_DB_NAME || 'userdatabase',
          },
        ],
      },
      entities: [__dirname + '/../**/*.entity{.ts,.js}'],
      synchronize: false, 
      logging: ['query', 'error'], 
    }),
  ],
  exports: [userModule],
})
export class AppModule {}
