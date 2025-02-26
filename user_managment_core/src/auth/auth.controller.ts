import { Controller, Post, Body, Request, UseGuards, UnauthorizedException } from '@nestjs/common';

import { ApiTags, ApiOperation, ApiBearerAuth } from '@nestjs/swagger';
import { CreateAuthDto } from 'src/user/dto/create-auth.dto';
import { CreateRegistracionDto } from 'src/user/dto/create-registracion.dto';
import { GetUser } from 'src/user/libs/decorators/getUser';
import { JwtAuthGuard } from 'src/user/libs/jwt-auth.guard';
import { AuthService } from 'src/user/user.repository.services/authRepository.service';



@ApiTags('Auth')
@Controller('auth')
export class AuthController {
  constructor(private readonly authService: AuthService) { }


  @Post('login')
  @ApiOperation({ summary: 'Login and receive a JWT token' })
  async login(@Body() body: CreateAuthDto) {

    const user = await this.authService.validateUser(body.email, body.password);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    return this.authService.login(user);
  }



  @Post('profile')
  @UseGuards(JwtAuthGuard)
  async getProfile(@Body() body: any, @GetUser() user: any): Promise<any> {

    if (!user) {
      throw new UnauthorizedException('User not found');
    }
    const res = await this.authService.getProfileById(user.email);

    return res;

  }

}






