import { Controller, Get, Post, Body, Patch, Param, Delete, Inject } from '@nestjs/common';

import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { UserRepository } from '../user.repository/userRepository.service';
import { ApiOperation } from '@nestjs/swagger';
import { RedisService } from '../user.services/redis.service';
import { UserInterface } from '../user.repository/contracts/user.repository.Interface';



@Controller('registracion')
export class RegistracionController {
  constructor(@Inject("UserInterface")private readonly registracionService: UserInterface,
  private readonly redisService:RedisService) {}

  @Post('register')
  @ApiOperation({ summary: 'Register a new user' })
  async register(@Body() body: CreateRegistracionDto) {
    
    await this.redisService.registerUser(body); //redis 
  
    return   await this.registracionService.register(body);

  }
  @Get()
  async findAll() {  
    return await this.registracionService.findAll();
  }

  @Get('findOne/:id')
  async findOne(@Param('id') id: string) {
     await this.redisService.findUser(id) //redis 
    return await this.registracionService.getProfileById(id);
  }

  @Patch(':id')
  async update(@Param('id') id: string, @Body() updateRegistracionDto: UpdateRegistracionDto) {
    await this.redisService.updateUser(id,updateRegistracionDto);
    return await this.registracionService.updateUser(+id, updateRegistracionDto);
  }

  @Delete('delete/:id')
  async remove(@Param('id') id: string) {
    console.log("Received ID:", id);  //  Debugging
    await this.redisService.delete(id);
    // return await this.registracionService.remove(+id);
  }
  
}
