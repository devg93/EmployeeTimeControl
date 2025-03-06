import { forwardRef, Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { JwtModule } from '@nestjs/jwt';
import { PassportModule } from '@nestjs/passport';

import { User } from 'src/user/entities/registracion.entity';
import { RedisModule } from 'src/rediscache/rediscache.module';
import { RegistracionController } from './user.controllers/user.controller';
import { AuthController } from './user.controllers/auth.controller';
import { UserCommandRepository } from './user.repository/CommandRepository';
import { UserQeuryRepository } from './user.repository/QeuryRepository';
import { UserWriteService } from './user.services/user.write.service ';
import { UserReadService } from './user.services/user.read.service';
import { JwtStrategy } from './libs/JwtStrategy';


@Module({
  imports: [
    forwardRef(() => RedisModule),
    TypeOrmModule.forFeature([User]),
    PassportModule.register({ defaultStrategy: 'jwt' }),
    JwtModule.register({
      secret: process.env.JWT_SECRET || 'default_secret_key',
      signOptions: { expiresIn: '1h' },
    }),
  ],
  controllers: [AuthController, RegistracionController],
  providers: [
    {
      provide: 'IuserQeuryRepository',
      useClass: UserQeuryRepository,
    },
    {
      provide: 'IuserCommandRepository',
      useClass: UserCommandRepository,
    },
    {
      provide: 'IuserWriteService',
      useClass: UserWriteService,
    },
    {
      provide: 'IuserReadService',
      useClass: UserReadService,
    },
    JwtStrategy, 
  ],
   //exports: [JwtStrategy, PassportModule],
})
export class userModule {}
