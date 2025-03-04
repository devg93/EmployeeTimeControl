import { Controller, Post, Body, Request, UseGuards, UnauthorizedException, Inject } from '@nestjs/common';

import { ApiTags, ApiOperation } from '@nestjs/swagger';
import { CreateAuthDto } from '../dto/create-auth.dto';
import { JwtAuthGuard } from '../libs/jwt-auth.guard';
import { GetUser } from '../libs/decorators/getUser';
import { IuserInterface } from '../user.repository/contracts/user.repository.Interface';






@ApiTags('Auth')
@Controller('auth')
export class AuthController {
  constructor(@Inject('IuserInterface') private readonly userServices: IuserInterface) { }


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
    return await this.userServices.getProfileByEmail(user.email);

  }

}






