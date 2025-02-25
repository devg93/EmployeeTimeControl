// import { Module } from '@nestjs/common';
// import * as dotenv from 'dotenv';
// import { ConfigModule } from '@nestjs/config';

// import { TypeOrmModule } from '@nestjs/typeorm';
// import { DataSource } from 'typeorm';
// import { userModule } from './user/user.module';

// dotenv.config();

// @Module({
//   imports: [
//     userModule,
//     ConfigModule.forRoot(),
//     TypeOrmModule.forRoot({
//       type: 'mysql',
//       replication: {
//         master: {
//           host: process.env.MASTER_DB_HOST,  
//           port: +process.env.DB_PORT || 3306,
//           username: process.env.MASTER_DB_USERNAME || 'root',
//           password: process.env.MASTER_DB_PASSWORD || 'password',
//           database: process.env.MASTER_DB_NAME || 'userdatabase',
//         },
//         slaves: [
//           {
//             host: process.env.REPLICA_DB_HOST,  
//             port: +process.env.DB_PORT || 3306,
//             username: process.env.REPLICA_DB_USERNAME || 'replica_user',
//             password: process.env.REPLICA_DB_PASSWORD || 'strongpassword',
//             database: process.env.REPLICA_DB_NAME || 'userdatabase',
//           },
//         ],
//       },
//       entities: [__dirname + '/../**/*.entity{.ts,.js}'],
//       synchronize: false, 
//       logging: ['query', 'error'], 
//     }),
//   ],
//   exports: [],
// })
// export class AppModule {}

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
    EventEmitterModule.forRoot(),
    userModule,
    ConfigModule.forRoot(),
    TypeOrmModule.forRoot({
      type: 'mysql',
    
          host: process.env.MASTER_DB_HOST,  
          port: +process.env.DB_PORT || 3306,
          username: process.env.MASTER_DB_USERNAME || 'root',
          password: process.env.MASTER_DB_PASSWORD || 'password',
          database: process.env.MASTER_DB_NAME || 'userdatabase',
          entities: [User], 
      synchronize: true, 
      // autoLoadEntities: true, 
        },
      ),
      RedisModule    
  ],
  exports: [],
  providers: [RedisModule],
})
export class AppModule {}
