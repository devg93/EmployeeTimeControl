import { Controller, Post, Body, Request, UseGuards, UnauthorizedException, Inject } from '@nestjs/common';

import { ApiTags, ApiOperation, ApiBearerAuth } from '@nestjs/swagger';

import { AuthRepository } from '../user.repository/authRepository.service';
import { CreateAuthDto } from '../dto/create-auth.dto';
import { JwtAuthGuard } from '../libs/jwt-auth.guard';
import { GetUser } from '../libs/decorators/getUser';
import { RedisService } from '../user.services/redis.service';
import { UserInterface } from '../user.repository/contracts/user.repository.Interface';



@ApiTags('Auth')
@Controller('auth')
export class AuthController {
  constructor(@Inject('UserInterface') private readonly userServices: UserInterface,private readonly redisService:RedisService) { }


  @Post('login')
  @ApiOperation({ summary: 'Login and receive a JWT token' })
  async login(@Body() body: CreateAuthDto) {

    const user = await this.userServices.validateUser(body.email, body.password);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }

    return await this.userServices.login(body.email, body.password);
  }



  @Post('getProfileByemail')
  @UseGuards(JwtAuthGuard)
  async getProfileByemail(@Body() body: any, @GetUser() user: any): Promise<any> {
    

    if (!user) {
      throw new UnauthorizedException('User not found');
    }
    // const resRedis = await this.redisService.findUser(user.email);
//  console.log(resRedis)
    return await this.userServices.getProfileByEmail(user.email);

    // return resRedis;

  }

}






