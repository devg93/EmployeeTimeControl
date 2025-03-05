import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AuthRepository } from './user.repository/authRepository.service';
import { JwtModule } from '@nestjs/jwt';
import { JwtStrategy } from './libs/JwtStrategy';

import { User } from 'src/user/entities/registracion.entity';
import { PassportModule } from '@nestjs/passport';

import { RegistracionController } from './user.controllers/user.controller';
import { AuthController } from './user.controllers/auth.controller';
import { UserRepository } from './user.repository/userRepository.service';


import { RedisModule } from 'src/rediscache/rediscache.module';
import { UserWriteService } from './user.services/user.write.service ';
import { UserReadService } from './user.services/user.read.service';
import { EventEmitterModule } from '@nestjs/event-emitter';




@Module({

  //*******************Imports Modules************************ */
  imports: [RedisModule,
    EventEmitterModule.forRoot(),

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
      provide: 'Userrepositoryinterface',
      useClass: AuthRepository

    }, AuthRepository,

    //******************* */
    {
      provide: 'Userrepositoryinterface',
      useClass: UserRepository
    }, UserRepository,
     //******************* */
    {
      provide: 'IuserWriteInterface',
      useClass: UserWriteService
    }, UserWriteService,
  //******************* */
    {
      provide: 'IuserReadInterface',
      useClass: UserReadService
    }, UserReadService,
 //******************* */

   JwtStrategy,
   RedisModule

  , 

  ],


})
export class userModule { }
