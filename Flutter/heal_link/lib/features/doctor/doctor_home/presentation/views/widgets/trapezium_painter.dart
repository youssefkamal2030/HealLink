import 'package:flutter/material.dart';

import '../../../../../../core/utils/function/app_colors.dart';

class TrapeziumPainter extends CustomPainter {
  @override
  void paint(Canvas canvas, Size size) {
    final fillPaint =
        Paint()
          ..color = AppColors.kBackgroundColor
          ..style = PaintingStyle.fill;

    final borderPaint =
        Paint()
          ..color = Color(0xffD2E3F9)
          ..strokeWidth = 1
          ..style = PaintingStyle.stroke;

    final path = Path();

    double topWidth = size.width * 0.77;
    double bottomWidth = size.width;
    double dx = (bottomWidth - topWidth) / 2;

    Offset topLeft = const Offset(0, 0);
    Offset topRight = Offset(size.width, 0);
    Offset bottomRight = Offset(dx + topWidth, size.height);
    Offset bottomLeft = Offset(dx, size.height);

    path.moveTo(topLeft.dx, topLeft.dy);
    path.lineTo(topRight.dx, topRight.dy);
    path.lineTo(bottomRight.dx, bottomRight.dy);
    path.lineTo(bottomLeft.dx, bottomLeft.dy);
    path.close();

    canvas.drawPath(path, fillPaint);

    canvas.drawLine(bottomLeft, topLeft, borderPaint); // Left side
    canvas.drawLine(topRight, bottomRight, borderPaint); // Right side
    canvas.drawLine(bottomLeft, bottomRight, borderPaint); // Bottom side
  }

  @override
  bool shouldRepaint(CustomPainter oldDelegate) => false;
}
