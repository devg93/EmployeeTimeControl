import { forwardRef, Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import {  UserQeuryRepository } from './user.repository/QeuryRepository';
import { JwtModule } from '@nestjs/jwt';
import { JwtStrategy } from './libs/JwtStrategy';

import { User } from 'src/user/entities/registracion.entity';
import { PassportModule } from '@nestjs/passport';

import { RegistracionController } from './user.controllers/user.controller';
import { AuthController } from './user.controllers/auth.controller';
import { UserCommandRepository } from './user.repository/CommandRepository';


import { RedisModule } from 'src/rediscache/rediscache.module';
import { UserWriteService } from './user.services/user.write.service ';
import { UserReadService } from './user.services/user.read.service';
import { EventEmitterModule } from '@nestjs/event-emitter';




@Module({

  //*******************Imports Modules************************ */
  imports: [forwardRef(() => RedisModule),
  
    TypeOrmModule.forFeature([User]),

    PassportModule.register({ defaultStrategy: 'jwt' }),

    JwtModule.register({
      secret: process.env.JWT_SECRET || 'default_secret_key',
      signOptions: { expiresIn: '1h' },
    }),
  ],


  //*******************Controller service objects************************ */
  controllers: [AuthController, RegistracionController],


//*******************Provaiders service objects************************ */
  providers: [

    {
      provide: 'IuserQeuryRepository',
      useClass: UserQeuryRepository

    },

    //******************* */
    {
      provide: 'IuserCommandRepository',
      useClass: UserCommandRepository
    },
     //******************* */
    {
      provide: 'IuserWriteService',
      useClass: UserWriteService
    },
  //******************* */
    {
      provide: 'IuserReadService',
      useClass: UserReadService
    },
  
  ],


})
export class userModule { }
