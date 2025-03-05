// TypeOrmModule.forRoot({
//   type: 'postgres',
//   replication: {
//     master: {
//       host: process.env.DB_HOST,
//       port: +process.env.DB_PORT || 5432,
//       username: process.env.DB_USER,
//       password: process.env.DB_PASSWORD,
//       database: process.env.DB_NAME,
//     },
//     slaves: [
//       {
//         host: process.env.REPLICA_DB_HOST,
//         port: +process.env.REPLICA_DB_PORT || 5432,
//         username: process.env.REPLICA_DB_USERNAME,
//         password: process.env.REPLICA_DB_PASSWORD,
//         database: process.env.REPLICA_DB_NAME,
//       },
//     ],
//   },
//   entities: [User],
//   synchronize: true,
//   logging: true,
// })




import { Module } from '@nestjs/common';
import * as dotenv from 'dotenv';
import { ConfigModule } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { DataSource } from 'typeorm';
import { userModule } from './user/user.module';
import { User } from './user/entities/registracion.entity';
import { EventEmitterModule } from '@nestjs/event-emitter';
import { RedisModule } from './rediscache/rediscache.module';

dotenv.config();

@Module({
  imports: [
    userModule,
    ConfigModule.forRoot(),
    TypeOrmModule.forRoot({
      type: 'postgres', 
      host: process.env.DB_HOST,  
      port: +process.env.DB_PORT || 5432, 
      username: process.env.DB_USER || 'root', 
      password: process.env.DB_PASSWORD || 'Dami_2022', 
      database: process.env.DB_NAME || 'userdatabase',
      entities: [User], 
      synchronize: true, 
      logging: true, // Optional: Enable logs
    }),
    RedisModule,
  ],
  exports: [],
  providers: [RedisModule],
})
export class AppModule {}
